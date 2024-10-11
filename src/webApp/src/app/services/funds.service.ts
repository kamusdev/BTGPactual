import { IFund } from '../models/fund';
import { HttpClientModule } from '@angular/common/http';
import { HttpClient } from '@angular/common/http';
import { forwardRef, Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FundsService {
  constructor(private http: HttpClient) {}

  path() {
    return 'funds';
  }

  getAll(): Observable<IFund[]> {
    return this.http.get<IFund[]>(`${environment.apiUrl}/${this.path()}`);
  }
}
