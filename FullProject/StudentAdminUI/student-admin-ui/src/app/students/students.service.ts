import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {Observable} from 'rxjs'
import { AddStudentRequest } from '../models/api-models/AddStudentRequest.model';
import { Student } from '../models/api-models/student.model';
import { updateStudentRequest } from '../models/api-models/updateStudentRequest.mode';

@Injectable({
  providedIn: 'root'
})
export class StudentsService {
  ///artık base url budur. Students vs daha sonra eklenecek.
  private baseApiUrl = 'https://localhost:44380'
  constructor(private httpClient: HttpClient) { }
  //Observable yapmamızın sebebi backend'ten veri gelmesi için async olsun diye..
  getStudents(): Observable<Student[]>{
    //Nereye istek atacağımız parantez içine yazılır.
    return this.httpClient.get<Student[]>(this.baseApiUrl+'/Student'); //STUDENT.C.TS'E ÇEKİLİR..
  }

  getStudent(studentId:string | null): Observable<Student>{
    return this.httpClient.get<Student>(this.baseApiUrl+'/Student/' +studentId); //STUDENT.C.TS'E ÇEKİLİR..
  }

  updateStudent(studentId:string, studentRequest:Student): Observable<Student>{
    const updateStudentRequest : updateStudentRequest = {
      firstName:studentRequest.firstName,
      lastName:studentRequest.lastName,
      dateOfBirth:studentRequest.dateOfBirth,
      email:studentRequest.email,
      mobile:studentRequest.mobile,
      genderId:studentRequest.genderId,
      physicalAddress:studentRequest.address.physicalAddress,
      postalAddress:studentRequest.address.postalAddress
    }
    return this.httpClient.put<Student>(this.baseApiUrl+'/Student/' +studentId,updateStudentRequest); //İçerisinde yolladığımız jsonbody
  }

  deleteStudent(studentId:string): Observable<Student>{
    return this.httpClient.delete<Student>(this.baseApiUrl+'/Student/' +studentId); //İçerisinde yolladığımız jsonbody
  }

  addStudent(studentRequest:Student): Observable<Student>{
    const addStudentRequest : AddStudentRequest = {
      firstName:studentRequest.firstName,
      lastName:studentRequest.lastName,
      dateOfBirth:studentRequest.dateOfBirth,
      email:studentRequest.email,
      mobile:studentRequest.mobile,
      genderId:studentRequest.genderId,
      physicalAddress:studentRequest.address.physicalAddress,
      postalAddress:studentRequest.address.postalAddress
    }
    return this.httpClient.post<Student>(this.baseApiUrl+'/Student/add' ,addStudentRequest); //Son kısım body
  }


}
