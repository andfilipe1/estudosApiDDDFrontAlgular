import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { MovimentoManual } from './models/movimento-manual';
import { MovimentoManualTable } from './models/movimento-manual-table';
import { MovimentoManualService } from './services/movimento-manual/movimento-manual.service';

export interface PeriodicElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
  lancamento: string;
}

const ELEMENT_DATA: PeriodicElement[] = [
  { lancamento: 'asdf', position: 1, name: 'Hydrogen', weight: 1.0079, symbol: 'H'},
  { lancamento: 'asdf', position: 2, name: 'Helium', weight: 4.0026, symbol: 'He'},
  { lancamento: 'asdf', position: 3, name: 'Lithium', weight: 6.941, symbol: 'Li'},
  { lancamento: 'asdf', position: 4, name: 'Beryllium', weight: 9.0122, symbol: 'Be'},
  { lancamento: 'asdf', position: 5, name: 'Boron', weight: 10.811, symbol: 'B'},
  { lancamento: 'asdf', position: 6, name: 'Carbon', weight: 12.0107, symbol: 'C'},
  { lancamento: 'asdf', position: 7, name: 'Nitrogen', weight: 14.0067, symbol: 'N'},
  { lancamento: 'asdf', position: 8, name: 'Oxygen', weight: 15.9994, symbol: 'O'},
  { lancamento: 'asdf', position: 9, name: 'Fluorine', weight: 18.9984, symbol: 'F'},
  { lancamento: 'asdf', position: 10, name: 'Neon', weight: 20.1797, symbol: 'Ne'},
];

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  movimentoForm: FormGroup; 

  isEnable: boolean;

  displayedColumns: string[] = ['mes', 'ano', 'codigoProduto', 'descricaoProduto', 'nrLancamento', 'descricao', 'valor'];
  dataSource$!: Observable<MovimentoManualTable[]>;
  
  constructor(private movimentoManualService: MovimentoManualService) {

    this.isEnable = false;

    this.movimentoForm = new FormGroup({
      codigoProduto: new FormControl({ value: '', disabled: !this.isEnable }),
      mes: new FormControl({ value: '', disabled: !this.isEnable }),
      ano: new FormControl({ value: '', disabled: !this.isEnable }),
      produto: new FormControl({ value: '', disabled: !this.isEnable }),
      cosif: new FormControl({ value: '', disabled: !this.isEnable }),
      valor: new FormControl({ value: '', disabled: !this.isEnable }),
      descricao: new FormControl({ value: '', disabled: !this.isEnable }),
    });
  }

  ngOnInit() {
    this.getMovimentos();
  }

  addMovimento() {

    const movimento: MovimentoManual = this.movimentoForm.value as MovimentoManual;

    this.movimentoManualService.post(movimento).then(() => this.getMovimentos());

    this.isEnable = false;
    this.clean();
  }

  getMovimentos() {
    this.dataSource$ = this.movimentoManualService.get();
  }

  getProducts() {
    this.dataSource$ = this.movimentoManualService.get();
  }

  toggleIsEnable() {
    this.isEnable = !this.isEnable;

    this.clean();
  }

  clean() {
    this.movimentoForm = new FormGroup({
      codigoProduto: new FormControl({ value: '', disabled: !this.isEnable }),
      mes: new FormControl({ value: '', disabled: !this.isEnable }),
      ano: new FormControl({ value: '', disabled: !this.isEnable }),
      produto: new FormControl({ value: '', disabled: !this.isEnable }),
      cosif: new FormControl({ value: '', disabled: !this.isEnable }),
      valor: new FormControl({ value: '', disabled: !this.isEnable }),
      descricao: new FormControl({ value: '', disabled: !this.isEnable }),
    });
  }

}
