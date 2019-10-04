SELECT Id, d1, d2, d3, d4, d5, d6, d7, d8, d9, d10, d11, d12, d13, d14, d15,
		Total11, Total12, Total13, Total14, Total15, TotalPremiacao, 
		convert(decimal(5,2), convert(decimal(6,2), (SELECT MAX(Id) FROM tbConcurso)) / convert(decimal(5,2), TotalPremiacao)) As MediaPremiacao,
		Atraso01, Atraso02, Atraso03, Atraso04, Atraso05, AtrasoAtual
FROM tbCombinacao C
WHERE C.Atraso01 IS NOT NULL
AND C.Atraso01 - C.Atraso05 < 15
AND C.AtrasoAtual > C.Atraso05
AND C.TotalPremiacao > 240
ORDER BY MediaPremiacao 



SELECT Id, d1, d2, d3, d4, d5, d6, d7, d8, d9, d10, d11, d12, d13, d14, d15,
		Total11, Total12, Total13, Total14, Total15, TotalPremiacao, 
		convert(decimal(5,2), convert(decimal(6,2), (SELECT MAX(Id) FROM tbConcurso)) / convert(decimal(5,2), TotalPremiacao)) As MediaPremiacao,
		Atraso01, Atraso02, Atraso03, Atraso04, Atraso05, AtrasoAtual
FROM tbCombinacao C
WHERE C.Atraso01 IS NOT NULL
AND C.TotalPremiacao > 240
ORDER BY MediaPremiacao 


select * from tbConcurso


260055	1	2	3	4	6	9	10	11	13	15	17	20	23	24	25	209	36	8	NULL	NULL	253	7.27	31	31	30	29	28	31
