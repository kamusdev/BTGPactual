import { IOpening } from '../models/opening';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OpeningService {
  constructor(private http: HttpClient) {}

  path() {
    return 'opening';
  }

  addOpening(model: IOpening): Observable<IOpening> {
    return this.http.post<IOpening>(`${environment.apiUrl}/${this.path()}`, model);
  }
}
