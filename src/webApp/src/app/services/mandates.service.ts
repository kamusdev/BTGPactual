import { IMandate } from '../models/mandate';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MandatesService {
  constructor(private http: HttpClient) {}

  path() {
    return 'mandates';
  }

  getAll(): Observable<IMandate[]> {
    return this.http.get<IMandate[]>(`${environment.apiUrl}/${this.path()}`);
  }
}
