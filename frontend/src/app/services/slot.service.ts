import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class SlotService {

  constructor(private http:HttpClient) { }

  getDeliverySlots(payload: any): Observable<any[]> {
  return this.http.post<any[]>('https://localhost:7252/api/Delivery/slots', payload, {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  });
  }
 
}
