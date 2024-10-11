import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ToastrModule } from 'ngx-toastr';
import { forkJoin } from 'rxjs';
import { IClient } from 'src/app/models/client';
import { IClosure } from 'src/app/models/closure';
import { IMandate } from 'src/app/models/mandate';
import { ClientsService } from 'src/app/services/clients.service';
import { ClosureService } from 'src/app/services/closure.service';
import { MandatesService } from 'src/app/services/mandates.service';
import { SharedModule } from 'src/app/theme/shared/shared.module';

@Component({
  selector: 'app-closures',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './closures.component.html',
  styleUrl: './closures.component.scss',
  providers: [MandatesService, ClientsService, ClosureService, ToastrService]
})
export default class ClosuresComponent implements OnInit {
  tmpMandates: IMandate[] = [];
  tmpClients: IClient;
  dataClosure: any;
  dataMandate: any;
  isLoading: boolean = true; // Para gestionar la carga de datos
  private cdr: ChangeDetectorRef; // Inyecta ChangeDetectorRef

  constructor(
    private mandateService: MandatesService,
    private clientService: ClientsService,
    private closureService: ClosureService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.mandateService.getAll().subscribe({
      next: (data) => {
        this.tmpMandates = data; // Asigna los datos recibidos a la variable
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
  }

  Cancelacion(mandate: any) {
    //console.log('Datos Seleccionados', mandate);

    let closure: IMandate = mandate as IMandate;

    let model: IClosure = {
      clientId: closure.ClientId,
      fundId: closure.FundId,
      operationType: 2,
      mandateId: closure.Id,
      date: new Date(),
      value: closure.Value
    };

    // //Se procede a guardar el registro.
    // this.closureService.addClosure(model).subscribe(
    //   (response) => {
    //     this.toastr.success('La Cancelación fue creada correctamente');
    //   },
    //   (error) => {
    //     this.toastr.error('Error creando la cancelación');
    //   }
    // );

    // // Se cargan los datos nuevamente.
    // this.mandateService.getAll().subscribe({
    //   next: (data) => {
    //     this.tmpMandates = data; // Asigna los datos recibidos a la variable
    //   },
    //   error: (error) => {
    //     console.error('Error al obtener las transacciones:', error); // Manejo de errores
    //   },
    //   complete: () => {
    //     console.log('La llamada a la API se completó con éxito'); // Opcional: se ejecuta cuando la secuencia se completa
    //   }
    // });

    forkJoin({
      closureData: this.closureService.addClosure(model), // Llama al primer servicio
      mandateData: this.mandateService.getAll() // Llama al segundo servicio
    }).subscribe({
      next: (results) => {
        // Aquí obtienes los resultados de ambos servicios
        this.dataClosure = results.closureData;
        this.dataMandate = results.mandateData;
        this.isLoading = false; // Indica que ya terminó de cargar

        this.tmpMandates = this.dataMandate;

        // Forzar la detección de cambios manualmente
        //this.cdr.detectChanges();

        this.toastr.success('La Cancelación fue creada correctamente');
      },
      error: (error) => {
        console.error('Error al obtener datos:', error);
        this.isLoading = false; // En caso de error, también para el loading
      }
    });
  }
}
