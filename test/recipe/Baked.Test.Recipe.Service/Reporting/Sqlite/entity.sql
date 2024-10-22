SELECT
  count(Id),
  :name
FROM
  Entity
WHERE
  String LIKE :name
