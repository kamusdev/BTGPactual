import { IClosure } from '../models/closure';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ClosureService {
  constructor(private http: HttpClient) {}

  path() {
    return 'closure';
  }

  addClosure(model: IClosure): Observable<IClosure> {
    return this.http.post<IClosure>(`${environment.apiUrl}/${this.path()}`, model);
  }
}
