select count(*) from tbCombinacao
select count(*) from tbConcurso
select count(*) from tbCombinacaoConcurso

truncate table tbCombinacaoConcurso

select distinct IdCombinacao From tbCombinacaoConcurso
select * From tbCombinacaoConcurso


SELECT A.*, (SELECT MAX(Id) FROM tbConcurso) - A.IdConcurso As Concursos
FROM
	(select CC.IdCombinacao, max(CC.IdConcurso) As IdConcurso, max(C.DataSorteio) As DataSorteio, 
	DATEDIFF(DAY, max(C.DataSorteio), getdate()) As Dias
	from tbCombinacaoConcurso CC
	join tbConcurso C on C.Id = CC.IdConcurso
	where CC.p11 = 1 and CC.p12 = 0 and CC.p13 = 0 and CC.p14 = 0 and CC.p15 = 0
	group by CC.IdCombinacao) A
ORDER BY
A.Dias DESC


DBCC SHOWCONTIG
select * from tbCombinacao where Id = 25128

SELECT MAX(IdCombinacao) As UltimoLote FROM tbCombinacaoConcurso





