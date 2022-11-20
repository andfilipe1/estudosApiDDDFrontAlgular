import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom, Observable, throwError } from 'rxjs';
import { catchError, map, retry } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { MovimentoManual } from 'src/app/models/movimento-manual';
import { MovimentoManualTable } from 'src/app/models/movimento-manual-table';
interface Response1
{result:any;value:MovRes[];}
interface MovRes{daT_MES:number;daT_ANO:number,coD_PRODUTO:number,deS_PRODUTO:string,deS_DESCRICAO:string,vaL_VALOR:number, nuM_LANCAMENTO:number}


@Injectable({
  providedIn: 'root'
})
export class MovimentoManualService {

  apiUrl: string;

  constructor(private http: HttpClient) {
    this.apiUrl = environment.apiUrl + 'Product/';
  }

  get(): Observable<MovimentoManualTable[]> {
    return this.http.get<Response1>(this.apiUrl + 'GetAll')
      .pipe(
        map((data:Response1) => {
          
          return data.value.map(m =>{ 
            return{      
              mes: m.daT_MES,
              ano: m.daT_ANO,
              codigoProduto: m.coD_PRODUTO,
              descricaoProduto: m.deS_PRODUTO,
              nrLancamento: m.nuM_LANCAMENTO,
              descricao: m.deS_DESCRICAO,
              valor: m.vaL_VALOR
            }   as MovimentoManualTable 
          }
          )
        })
      );
  }

  async post(movimentoManual: MovimentoManual): Promise<void> {
    return await firstValueFrom<void>(this.http.post<void>(this.apiUrl + 'InsertProduct', 
{
  "descProduto": movimentoManual.descricao,
  "status": 'S',
  "pordutoCosif": {
    "codClassificacao": "23423"
  },
  "movimentoManual": {
    "dataMes": movimentoManual.mes,
    "dataAno": movimentoManual.ano,
    "descricao": movimentoManual.descricao,
    "idUsuario": "1",
    "valValor": movimentoManual.valor
  }
}, {}));
  }

}
