SELECT
  count(e.Id),
  e.String
FROM
  Entity e
WHERE
  e.String LIKE :name
GROUP BY
  e.String
