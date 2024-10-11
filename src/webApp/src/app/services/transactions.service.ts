import { ITransaction } from '../models/transaction';
import { HttpClientModule } from '@angular/common/http';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TransactionsService {
  constructor(private http: HttpClient) {}

  path() {
    return 'transactions';
  }

  getAll(): Observable<ITransaction[]> {
    return this.http.get<ITransaction[]>(`${environment.apiUrl}/${this.path()}`);
  }
}
