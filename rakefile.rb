require 'rake/clean'

SELF_PATH = File.dirname(__FILE__)
PATH_TO_MSBUILD = "C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\msbuild.exe"
PATH_TO_SQL = "#{SELF_PATH}\\_sql\\setup\\"
PATH_TO_WEB = "#{SELF_PATH}\\Platform.Lite"
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
    system("#{PATH_TO_MSBUILD} /v:q /p:Configuration=#{config} /t:TransformWebConfig #{SELF_PATH}/Platform.Lite/Platform.Lite.csproj")
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

namespace "setup" do
  desc "creates a new platform lite client using the known and expected conventions"
  task :client, :client_name, :client_domain do |t,args|
    puts "Expected usage: rake setup:client[ClientName, ClientDomain (optional)]"
    
    domain = args.client_domain ? args.client_domain : "http://localhost/#{args.client_name}/"
    client_dir = "Areas/#{args.client_name}"

    if !File.directory? client_dir then
      begin
        database_setup(args.client_name, domain)
        create_directory(args.client_name)
      rescue Exception=>e
        puts e
      end
    else
      system("cls")
      puts "The directory #{client_dir} already exists.\n\n" 
      puts "Client names must be unique, please choose a new name and run the setup command again"
    end
  end

  def database_setup(client_name, client_domain)
        puts "\nCreating #{client_name} Database"
        system("sqlcmd -S localhost -Q \"CREATE DATABASE #{client_name}\"")

        puts "\nSeeding #{client_name} database with default data" 
        system("sqlcmd -S localhost -d #{client_name} -i \"#{PATH_TO_SQL}bootstrap.sql\"")

        puts "\nUpdating #{client_name} layout paths"
        system("sqlcmd -S localhost -d #{client_name} -Q \"UPDATE Layout SET LayoutFile = '~/Areas/#{client_name}/Views/Shared/Homepage.master' WHERE FriendlyName = 'Home Page Layout';\"")
        system("sqlcmd -S localhost -d #{client_name} -Q \"UPDATE Layout SET LayoutFile = '~/Areas/#{client_name}/Views/Shared/OneColumn.master' WHERE FriendlyName = 'One Column Layout';\"")

        puts "\nAdding #{client_name} record to master database"
        system("sqlcmd -S localhost -d PlatformMaster -Q \"INSERT INTO SITE(DomainNames, ClientDirectory, ClientName, SiteName, ConnectionString, DateCreated, IsActive) VALUES ('#{client_domain}', 'Areas/#{client_name}/', '#{client_name}', '#{client_name}', 'Data Source=(local);Initial Catalog=#{client_name};Persist Security Info=True;User ID=sa;Password=yomama81', getDate(), '1');\"")
  end

  def create_directory(client_name) 
    puts "\nCreatng client directory and populating with default data"

    new_directory = ".\\Platform.Lite\\Areas\\#{client_name}"
    Dir.mkdir(new_directory) if !File.directory? new_directory

    FileUtils.cp_r "Platform.Lite/Areas/DefaultClient/Public/.", "Platform.Lite/Areas/#{client_name}/Public" 
    FileUtils.cp_r "Platform.Lite/Areas/DefaultClient/Views/.", "Platform.Lite/Areas/#{client_name}/Views" 
    FileUtils.cp "Platform.Lite/Areas/DefaultClient/DefaultClientAreaRegistration.cs", "Platform.Lite/Areas/#{client_name}/#{client_name}AreaRegistration.cs" 

    area_file = File.new("Platform.Lite/Areas/#{client_name}/#{client_name}AreaRegistration.cs", "w")
    area_file.write(create_arearegistration_file(client_name))
    area_file.close
  end

  def create_arearegistration_file(client_name)
    area_reg =  "using System.Web.Mvc;\n\n"
    area_reg += "namespace Platform.Lite.Areas.DefaultClient {\n"
    area_reg += "    public class CLIENT_NAMEAreaRegistration : AreaRegistration {\n"
    area_reg += "        public override string AreaName {\n"
    area_reg += "            get {\n"
    area_reg += "                return \"CLIENT_NAME\";\n"
    area_reg += "            }\n"
    area_reg += "        }\n\n"
    area_reg += "        public override void RegisterArea(AreaRegistrationContext context) {\n"
    area_reg += "            context.MapRoute(\"CLIENT_NAME_default\",\"CLIENT_NAME/{controller}/{action}/{id}\",new { action = \"Index\", id = UrlParameter.Optional });\n"
    area_reg += "        }\n"
    area_reg += "    }\n"
    area_reg += "}\n"

    return area_reg.gsub(/CLIENT_NAME/, client_name)
  end
end

namespace "teardown" do
  desc "destroys all traces of a client setup"
  task :client, :client_name, :bool_force do |t,args|
    client_dir = "Platform.Lite/Areas/#{args.client_name}"

    if File.directory? client_dir then
      args.bool_force == "false" if args.bool_force == ""

      system("iisreset") if args.bool_force == "true"
      
      puts "\nRemoving #{args.client_name} entry from master database"
      system("sqlcmd -S localhost -d PlatformMaster -Q \"DELETE FROM Site WHERE ClientName = '#{args.client_name}'\"")

      puts "\nRemoving #{args.client_name} client directory"
      FileUtils.rm_r("Platform.Lite/Areas/#{args.client_name}")

      begin
        puts "\nDropping  #{args.client_name} database"
        system("sqlcmd -S localhost -Q \"DROP DATABASE [#{args.client_name}]\"")
      rescue Exception=>e
        puts "There was a problem dropping the database"
      end
      

    else
      system("cls")
      puts "That client doesn\'t exists, nothing to teardown"
    end
  end
end
