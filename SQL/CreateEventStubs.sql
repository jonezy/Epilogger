Select * from Events

Update Events set EventStub=
Select replace(replace(replace(replace(replace(
replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(Name
			, ' ', '')
			, '@', '')
			, '.', '')
			, '''', '')
			, ':', '') 
			, '/', '')
			, '!', '')
			, '#', '')
			, '(', '')
			, ')', '')
			, '-', '')
			, ',', '')
			, '<', '')
			, '&', '')
			, '/', '')			
from Events



UPDATE
    Events
SET
    Events.EventStub = replace(replace(replace(replace(replace(
replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(Events.Name
			, ' ', '')
			, '@', '')
			, '.', '')
			, '''', '')
			, ':', '') 
			, '/', '')
			, '!', '')
			, '#', '')
			, '(', '')
			, ')', '')
			, '-', '')
			, ',', '')
			, '<', '')
			, '&', '')
			, '/', '')
FROM
    Events




/ ! # ()- , < &

