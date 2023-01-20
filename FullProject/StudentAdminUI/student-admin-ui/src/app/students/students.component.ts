import { Component, OnInit,ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { Student } from '../models/ui-models/student.model';
import { StudentsService } from './students.service';

@Component({
  selector: 'app-students',
  templateUrl: './students.component.html',
  styleUrls: ['./students.component.css']
})
export class StudentsComponent implements OnInit {
  students:Student[]=[];   //Dikkat ui-models'de ki Student seçilir. Mapleme yapıldı.
  displayedColumns: string[] = ['firstName', 'lastName', 'DateOfBirth', 'email','mobile', 'gender', 'edit'];
  dataSource:MatTableDataSource<Student> = new MatTableDataSource<Student>() //ui tarafından gelen student
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  filterString= "";

  constructor(private studentService: StudentsService) { }

  ngOnInit() {
    this.studentService.getStudents().subscribe( //datamessage döndürürüz. issuccess döndürürüz..
      (success: any) => { //success'de 45 data geliyordu. Students'e atıyoruz.
        this.students = success; //Sağdaki api-models soldaki ui-models fakat proportyleri aynı old. için mapleme işlemi oldu.
        this.dataSource = new MatTableDataSource<Student>(this.students); //Artık bu datayı student tipinde çeviriyor ve datasource'e atıyor.
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      },
      (err: any) =>
      {

      }
    )
  }

  filterStudents()
  {  //dataSource'de arama yapacak.
    this.dataSource.filter = this.filterString.trim().toLocaleLowerCase(); //küçük harf boşluksuz.. ngModel ile..
  }
}
