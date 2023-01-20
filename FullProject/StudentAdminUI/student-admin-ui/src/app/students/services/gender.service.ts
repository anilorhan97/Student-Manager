import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Gender } from 'src/app/models/api-models/gender.model';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GenderService {
  private baseApiUrl = 'https://localhost:44380';

  constructor(private httpClient: HttpClient) { }

  getGenderList(): Observable<Gender[]>{
    //Nereye istek atacağımız parantez içine yazılır.
    return this.httpClient.get<Gender[]>(this.baseApiUrl+'/genders'); //STUDENT.C.TS'E ÇEKİLİR..
  }
}
