CREATE OR ALTER PROCEDURE PR_LIS_LISTA_MOVIMENTOS_MANUAIS
AS
SELECT C.DAT_MES, C.DAT_ANO, P.COD_PRODUTO, P.DES_PRODUTO, C.NUM_LANCAMENTO, C.DEs_DESCRICAO, C.VAL_VALOR 
FROM MOVIMENTO_MANUAL AS C
JOIN PRODUTO AS P ON C.COD_PRODUTO = P.COD_PRODUTO