Select * from Events

--Update Events set EventStub=
--Select replace(replace(replace(replace(replace(
--replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(Name
--			, ' ', '')
--			, '@', '')
--			, '.', '')
--			, '''', '')
--			, ':', '') 
--			, '/', '')
--			, '!', '')
--			, '#', '')
--			, '(', '')
--			, ')', '')
--			, '-', '')
--			, ',', '')
--			, '<', '')
--			, '&', '')
--			, '/', '')			
--from Events



UPDATE
    Events
SET
    Events.EventSlug = lower(replace(replace(replace(replace(replace(
replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(Events.Name
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
			, ' ', '-'))
FROM
    Events

Select * from Events

Update Events Set EventSlug='govcamp-canada-2011' Where ID=130
Update Events Set EventSlug='epilogger' Where ID=133
Update Events Set EventSlug='red-bull-music-academy-world-tour--culture-clash-TO' Where ID=213
Update Events Set EventSlug='techdays-2011' Where ID=169
Update Events Set EventSlug='love-a-heart' Where ID=361






/ ! # ()- , < &

exec [GetHomePageActivity]

Select * from ActivityLatestEventsCreated

Select * from ActivityLatestImages


Select * from [User] Where Username='cbrooker'
exec GetUserDashboardActivity 'AFA23475-0971-4795-BDDC-70F5437150FE'


DECLARE 	@FromUserScreenName varchar(500)
	SET @FromUserScreenName = (SELECT ServiceUsername FROM UserAuthenticationProfile WHERE UserID = @UserID AND Service = 'TWITTER')
	
	-- get twitter activity
	SELECT	2 as ActivityType,
			t.CreatedDate as [Date], 
			t.TextAsHTML as ActivityContent, 
			e.Name as EventName, e.id as EventID
	FROM Tweets t
	JOIN Events e on e.ID = t.EventID
	WHERE FromUserScreenName = 'cbrooker'
	
	SELECT	3 as ActivityType,
			e.CreatedDateTime as [Date],
			e.Name as ActivityContent,
			e.Name as EventName, e.id as EventID
	FROM	[Events] e
	WHERE	UserID = 'AFA23475-0971-4795-BDDC-70F5437150FE'
	
	SELECT	5 as ActivityType,
			ufe.Timestamp as [Date],
			e.Name as ActivityContent,
			e.Name as EventName,
			e.ID as EventID
	FROM	UserFollowsEvent ufe, [Events] e
	WHERE	ufe.UserID = 'AFA23475-0971-4795-BDDC-70F5437150FE'  and e.ID=ufe.EventID
	
	
		SELECT	1 as ActivityType,
			imd.DateTime as [Date],
			(SELECT FullSize FROM Images WHERE ID = imd.ImageID) as ActivityContent,
			(SELECT e.Name FROM Events e WHERE e.ID = imd.EventID) as EventName,
			(SELECT e.id FROM Events e WHERE e.ID = imd.EventID) as EventID
	FROM	ImageMetaData imd
	WHERE	imd.UserID = 'AFA23475-0971-4795-BDDC-70F5437150FE'
	
	
		-- event ratings
	SELECT	4 as ActivityType,
			ure.RatingDateTime as [Date],
			convert(varchar, ure.UserRating) as ActivityContent,
			(SELECT e.Name FROM Events e WHERE e.ID = ure.EventID) as EventName,
			(SELECT e.ID FROM Events e WHERE e.ID = ure.EventID) as EventID
	FROM	UserRatesEvent as ure
	WHERE	ure.UserID = 'AFA23475-0971-4795-BDDC-70F5437150FE' 