import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './components/home/home.component';
import { MyTestsComponent } from './components/my-tests/my-tests.component';
import {JwtModule} from "@auth0/angular-jwt";
import { HttpClientModule } from "@angular/common/http"
import { acces_Token_Key } from './services/auth.service';
import {environment } from "src/environments/environment";
import { LoginComponent } from './components/login/login.component'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from "./services/auth.service";
import { TestFormComponent } from './test-form/test-form.component';

export function tokenGetter() {
  var str: string = "";
  str += localStorage.getItem(acces_Token_Key);
  return str;
}


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    MyTestsComponent,
    LoginComponent,
    TestFormComponent,
    //HttpClientModule,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:5001"],
        disallowedRoutes:[]
        }
    }),

  ],
  providers: [AuthService],
  bootstrap: [AppComponent]
})
export class AppModule { }
