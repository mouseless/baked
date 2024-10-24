SELECT
  count(e.Id),
  e.String
FROM
  Entity e
WHERE
  e.String LIKE :string
GROUP BY
  e.String
