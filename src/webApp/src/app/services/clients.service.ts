import { IClient } from '../models/client';
import { HttpClient } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ClientsService {
  constructor(private http: HttpClient) {}

  path() {
    return 'clients/1';
  }

  getAll(): Observable<IClient> {
    return this.http.get<IClient>(`${environment.apiUrl}/${this.path()}`);
  }
}
