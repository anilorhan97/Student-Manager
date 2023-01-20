import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Gender } from 'src/app/models/ui-models/gender.model';
import { Student } from 'src/app/models/ui-models/student.model';
import { GenderService } from '../services/gender.service';
import { StudentsService } from '../students.service';

@Component({
  selector: 'app-view-student',
  templateUrl: './view-student.component.html',
  styleUrls: ['./view-student.component.css']
})
export class ViewStudentComponent implements OnInit {
  studentId:string | null | undefined;
  student : Student = { //Başlangıç değerleri..
    id : '',
    firstName : '',
    lastName : '',
    dateOfBirth: '',
    email : '',
    mobile : 0,
    genderId: '',
    profileImageUrl: '',
    gender: {
      id:'',
      description: ''
    },
    address: {
      id: '',
      physicalAddress: '',
      postalAddress: '',
    }
  };
  //Burada ui model'de ki Gender kullanılıyor. Service de api-model'de ki Gender kullanılıyor.
  genderList : Gender[] = [];
  isNewStudent = false;
  header = "";

  constructor(private readonly studentService: StudentsService,
    private readonly genderService: GenderService,
    private readonly route:ActivatedRoute,
    private router: Router,
    private snackbar: MatSnackBar
    ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(
      (params) => {
        this.studentId = params.get('id');
        //service.ts'e studentId'i string olarak tanımlamıştık | null eklemeliyiz.

        //studentId add ise eklemeye göre
        if(this.studentId === "add")
        {
          this.isNewStudent = true;
          this.header = "Öğrenci Ekle";
        }
        else{
          this.isNewStudent = false;
          this.header = "Öğrenciyi Düzenle";
          this.studentService.getStudent(this.studentId).subscribe(
            (success) => {
              this.student = success;
            },
            (error) => {
            }
          )
        }
        //değilse edite göre
        this.genderService.getGenderList().subscribe(
          (success) => {
            this.genderList = success;
          },
          (error) => {

          }
        )
      }
    )
  }

  onUpdate()
  {
    this.studentService.updateStudent(this.student.id,this.student)
    .subscribe(
      (success) =>{
        this.snackbar.open("Öğrenci başarılı bir şekilde güncellendi", undefined,{
          duration:3000
        })
        this.router.navigateByUrl('/');
      },
      (error) => {
        this.snackbar.open("Öğrenci güncellerken bir hata meydana geldi.", undefined,{
          duration:3000
        })
      }
    )
  }

  onDelete()
  {
    this.studentService.deleteStudent(this.student.id).subscribe(
      (success) => {
        this.snackbar.open("Öğrenci başarılı bir şekilde silindi", undefined,{
          duration:3000
        })
        this.router.navigateByUrl('/');
      },
      (error) => {

      }
    )
  }

  onAdd()
  {
    this.studentService.addStudent(this.student)
    .subscribe(
      (success) =>{
        this.snackbar.open("Öğrenci başarılı bir şekilde eklendi", undefined,{
          duration:3000
        })
        this.router.navigateByUrl(`/${success.id}`); //Öğrenci edit etme sayfasına yönlenmiş olacak.
      },
      (error) => {
        this.snackbar.open("Öğrenci eklerken bir hata meydana geldi.", undefined,{
          duration:3000
        })
      }
    )
  }
}
