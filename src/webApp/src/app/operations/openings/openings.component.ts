import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ToastrModule } from 'ngx-toastr';
import { IClient } from 'src/app/models/client';
import { IFund } from 'src/app/models/fund';
import { IOpening } from 'src/app/models/opening';
import { ClientsService } from 'src/app/services/clients.service';
import { FundsService } from 'src/app/services/funds.service';
import { OpeningService } from 'src/app/services/opening.service';
import { SharedModule } from 'src/app/theme/shared/shared.module';

@Component({
  selector: 'app-openings',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './openings.component.html',
  styleUrl: './openings.component.scss',
  providers: [FundsService, ClientsService, OpeningService, ToastrService]
})
export default class OpeningsComponent implements OnInit {
  tmpfunds: IFund[] = [];
  tmpClients: IClient;
  currentDate: Date;
  strCurrentDate: string;
  IdFund: number = 0;
  Fund: IFund;
  formGroup!: UntypedFormGroup;
  ClienteName: string;

  constructor(
    private fundsService: FundsService,
    private clientService: ClientsService,
    private openingService: OpeningService,
    private toastr: ToastrService,
    private fb: UntypedFormBuilder
  ) {
    this.currentDate = new Date();
    this.strCurrentDate = this.formatDate(this.currentDate);
  }

  ngOnInit(): void {
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

    this.clientService.getAll().subscribe({
      next: (data) => {
        this.tmpClients = data; // Asigna los datos recibidos a la variable
        //this.ClienteName = this.tmpClients[0].Name;
      },
      error: (error) => {
        console.error('Error al obtener los Clientes:', error); // Manejo de errores
      },
      complete: () => {
        console.log('La llamada a la API se completó con éxito'); // Opcional: se ejecuta cuando la secuencia se completa
      }
    });

    this.formGroup = this.fb.group({
      valorApertura: [0, [Validators.required]],
      fechaApertura: [this.strCurrentDate]
    });

    this.formGroup.get('fechaApertura')?.disable();
    this.IdFund = 1;
    this.Fund = this.tmpfunds.find((f) => f.Id == 1);
    //this.fondoChanged(this.Fund);
    //console.log(this.Fund);
  }

  formatDate(date: Date): string {
    const year = date.getFullYear(); // Obtiene el año
    const month = ('0' + (date.getMonth() + 1)).slice(-2); // Obtiene el mes (agrega 1 y asegura 2 dígitos)
    const day = ('0' + date.getDate()).slice(-2); // Obtiene el día (asegura 2 dígitos)
    return `${year}-${month}-${day}`; // Retorna la fecha formateada en "YYYY-MM-DD"
  }

  fondoChanged(event: any): void {
    if (event != null) {
      this.IdFund = Number(event.target.value);
      this.Fund = this.tmpfunds.find((f) => f.Id == this.IdFund);
    }
  }

  saveRecord(): void {
    if (this.formGroup.get('valorApertura').value == '') {
      this.toastr.warning('El valor de la Apertura debe ser mayor a 0');
      return;
    }

    if (this.formGroup.get('valorApertura').value == '0') {
      this.toastr.warning('El valor de la Apertura debe ser mayor a 0');
      return;
    }

    let model: IOpening = {
      clientId: this.tmpClients.Id,
      operationType: 1,
      fundId: this.IdFund,
      date: new Date(),
      value: this.formGroup.get('valorApertura').value
    };

    console.log(model);

    if (model.value > this.tmpClients.Balance) {
      this.toastr.warning('El valor de la Apertura supera el saldo disponible');
      return;
    }

    if (model.value < this.Fund.MinValue) {
      const formatoMoneda = new Intl.NumberFormat('es-CO', {
        style: 'currency',
        currency: 'COP',
        minimumFractionDigits: 2
      }).format(this.Fund.MinValue);

      this.toastr.warning('El mínimo de valor de apertura para este fondo es de ' + formatoMoneda);
      return;
    }

    //Se procede a guardar el registro.
    this.openingService.addOpening(model).subscribe(
      (response) => {
        this.toastr.success('La Apertura fue creada correctamente');
      },
      (error) => {
        this.toastr.error('Error creando la apertura');
      }
    );
  }

  validarNumeros(event: KeyboardEvent): void {
    const charCode = event.which ? event.which : event.keyCode;
    // Permitir solo números (48-57 en ASCII)
    if (charCode < 48 || charCode > 57) {
      event.preventDefault();
    }
  }
}
