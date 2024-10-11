import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ToastrModule } from 'ngx-toastr';
import { IFund } from 'src/app/models/fund';
import { ITransaction } from 'src/app/models/transaction';
import { FundsService } from 'src/app/services/funds.service';
import { TransactionsService } from 'src/app/services/transactions.service';
import { SharedModule } from 'src/app/theme/shared/shared.module';

@Component({
  selector: 'app-transactions',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.scss',
  providers: [TransactionsService, FundsService, ToastrService]
})
export default class TransactionsComponent implements OnInit {
  tmptransactions: ITransaction[] = [];
  tmpfunds: IFund[] = [];

  constructor(
    private trasanctionService: TransactionsService,
    private fundsService: FundsService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.trasanctionService.getAll().subscribe({
      next: (data) => {
        this.tmptransactions = data; // Asigna los datos recibidos a la variable
      },
      error: (error) => {
        console.error('Error al obtener las transacciones:', error); // Manejo de errores
      },
      complete: () => {
        console.log('La llamada a la API se completó con éxito'); // Opcional: se ejecuta cuando la secuencia se completa
        //this.toastr.clear;
        //this.toastr.info('Se Cargaron los datos Correctamente', 'Inicio');
      }
    });

    this.fundsService.getAll().subscribe({
      next: (data) => {
        this.tmpfunds = data; // Asigna los datos recibidos a la variable
      },
      error: (error) => {
        console.error('Error al obtener los fondos:', error); // Manejo de errores
      },
      complete: () => {
        console.log('La llamada a la API se completó con éxito'); // Opcional: se ejecuta cuando la secuencia se completa
      }
    });
  }
}
