require 'rake/clean'

SELF_PATH = File.dirname(__FILE__)
PATH_TO_MSBUILD = "C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\msbuild.exe"
# MAKE SURE YOU UPDATE THIS.
# It's the only thing you need to change.
PROJECT_NAME = "Epilogger.Web"
PATH_TO_SQL = "#{SELF_PATH}\\_sql\\setup\\"
PATH_TO_WEB = "#{SELF_PATH}\\#{PROJECT_NAME}"
TARGET_ENV = "staging"

# list of files and directories to clean, change to suit your liking
CLEAN.exclude("**/core","**/_sql")
CLEAN.include("*.cache", "*.xml", "*.suo", "**/obj", "**/bin", "../Deploy")

$BUILD_FORMAT = "1.0.{s}"
$SVN_REVISION

task :default => :build

# builds all the .sln files in the directory
task :build, :config do |t, args| 
  desc "builds all of the .sln files in the current directory"
  config = !args.config ? "Debug" : args.config

  Dir.glob('*.sln') do |file|
    puts "\nBuilding #{file}"
    system("#{PATH_TO_MSBUILD} /v:q /p:Configuration=#{config} /t:TransformWebConfig #{PATH_TO_WEB}/#{PROJECT_NAME}.csproj")
  end
end

namespace "deploy" do
  desc "Preps the project for deployment"
  task :project, :project_name, :destination do |t, args|
    begin
      TARGET_ENV = args.destination if args.destination.to_s != ""
      config_file = "Web.config.#{TARGET_ENV}"

      Rake::Task["clean"].invoke # clean everything up
      Rake::Task["build"].invoke('Release') # build the project

      Dir.mkdir("../Deploy") if !File.exists?('../Deploy') 

      get_version(args.project_name)
      version_number = "1.0.#{$SVN_REVISION}"
      package_name = "#{args.project_name}.#{version_number}"

      # copies the main project files
      puts "\nCopying main project files to deploy directory"
      system("xcopy .\\#{args.project_name} ..\\Deploy\\#{package_name}\\ /S /C /Y /Q /exclude:e.txt")
     begin
        #copies the projects deployment specific config file
        puts "\nCopying configuration file to deploy directory"
        system("xcopy .\\#{args.project_name}\\obj\\Release\\TransformWebConfig\\transformed\\Web.config ..\\Deploy\\#{package_name}\\Web.config /S /C /Y /Q")
      rescue Exception=>e
        puts e
      end
    rescue Exception=>e
      puts e
    end
  end

  def get_version(project_name)
    $SVN_REVISION = %x[git log | grep ^commit | sed 's/commit//'].split[0]
  end
end

namespace "database" do

  desc "backup a database from your local sql express installation, just give us zzz name!"
  task :backup, :db_name do |t, args|

    begin
      system("sqlcmd -S (local) -Q \"BACKUP DATABASE #{args.db_name} TO  DISK = N'#{SELF_PATH}/_sql/#{args.db_name}.bak' WITH NOFORMAT, INIT,  NAME = N'#{args.db_name}-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10\"")
    rescue Exception=>e
      puts "Couldn't back up the database: " + e
    end
  end

end
