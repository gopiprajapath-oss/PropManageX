import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Unit } from '../models/unit-model';
import { environment } from '../../../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UnitService {
 
  private http = inject(HttpClient);
 
  private apiUrl = `${environment.apiUrl}/Unit`;
  //  GET ALL UNITS BY PROPERTY
 getUnitsByProperty(propertyId: number): Observable<Unit[]> {
  return this.http.get<Unit[]>(`${this.apiUrl}/property/${propertyId}`);
}

  //  GET UNIT BY ID
  getUnitById(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }


  addUnit(data:any):Observable<any>{
    const token =localStorage.getItem('token');
    return this.http.post(this.apiUrl,data,{
        headers:{
            Authorization:`Bearer ${token}`
        }
    })
  }
 
  //  UPDATE UNIT (ONLY STATUS)
  updateUnit(id: number, data: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, data);
  }

deleteUnit(id: number): Observable<any> {
    const token = localStorage.getItem('token');
    return this.http.delete(`${this.apiUrl}/${id}`, {
        headers: {
          Authorization: `Bearer ${token}`
        }
      });
  }

}
 