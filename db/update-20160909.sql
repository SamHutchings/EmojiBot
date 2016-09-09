ALTER TABLE "Emoji" ADD Variations VARCHAR(100);

UPDATE 
	"Emoji" em
SET 
	Variations = (SELECT string_agg(Characters, '') FROM "Emoji" e WHERE e.Name LIKE (em.Name || ', Type%'))
WHERE 
	em.Name NOT LIKE '%Type%';