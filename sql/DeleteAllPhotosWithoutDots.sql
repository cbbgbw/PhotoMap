

DELETE FROM Photo
WHERE CHARINDEX(',', Latitude) > 0
    OR CHARINDEX(',', Longitude) > 0