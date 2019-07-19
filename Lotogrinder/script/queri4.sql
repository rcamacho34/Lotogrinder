select * From tbCombinacao
where id in (
32864
,70548
,89721
,167395
,259800
,260055
,270301
,342489
,483335
,484464
,548819
,1235151
,2001022
,2119174
,2875810
)
order by AtrasoAtual desc

update tbCombinacao set 
IdUltimo11 = null,
IdUltimo12 = null,
IdUltimo13 = null,
IdUltimo14 = null,
IdUltimo15 = null,
Total11 = null,
Total12 = null,
Total13 = null,
Total14 = null,
Total15 = null,
TotalPremiacao = null,
Atraso01 = null,
Atraso02 = null,
Atraso03 = null,
Atraso04 = null,
Atraso05 = null,
AtrasoAtual= null
where id in (
32864
,70548
,89721
,167395
,259800
,260055
,270301
,342489
,483335
,484464
,548819
,1235151
,2001022
,2119174
,2875810
)

