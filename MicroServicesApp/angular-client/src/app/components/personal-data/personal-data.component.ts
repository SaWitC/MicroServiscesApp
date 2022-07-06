import { Component, OnInit } from '@angular/core';
import { AuthService } from "../../services/auth.service";
import { Route, Router } from "@angular/router"
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-personal-data',
  templateUrl: './personal-data.component.html',
  styleUrls: ['./personal-data.component.css']
})
export class PersonalDataComponent implements OnInit {

  constructor(private router: Router,
    public auth: AuthService,
    private http: HttpClient) { }

  ngOnInit(): void {
    //if (this.auth.invalidLogin == true) {
    //  this.router.navigate(["/"]);
    //}
  }

  test() {
    //this.http.get('http://localhost:9410/api/Costomisation').subscribe(
    //  res => {
    //    console.log(res)
    //  },
    //  err => {
    //  });

    this.http.get('http://localhost:46574/api/Costomisation').subscribe(res => {
      console.log(res)

    },
      err => {
        console.log(err)
      })
    }   



  logOut() {
    this.auth.logOut();
    this.auth.invalidLogin = true;
    this.router.navigate(["/"]);
  }
}
