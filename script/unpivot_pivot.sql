; with numbered as
(
	select *,
         row_number() over (order by (select null)) RecordNumber
	 from tbConcurso
),
ordered as 
(
	select *,
         row_number() over (partition by RecordNumber
                            order by v) rn
	
	from numbered

	unpivot (v for c in (d1, d2, d3, d4, d5, d6, d7, d8, d9, d10, d11, d12, d13, d14, d15)) u
)
--select * from ordered

select Id,
		[1] d1,
		[2] d2,
		[3] d3,
		[4] d4,
		[5] d5,
		[6] d6,
		[7] d7,
		[8] d8,
		[9] d9,
		[10] d10,
		[11] d11,
		[12] d12,
		[13] d13,
		[14] d14,
		[15] d15
from
(
	select Id,
	v,
	Rn
	from ordered
) o
pivot (min(v) for Rn in ([1], [2], [3], [4], [5], [6], [7], [8], [9], [10], [11], [12], [13], [14], [15])) p
order by Id
