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
import { TestFormComponent } from "./components/test-form/test-form.component"
import {AuthGuard} from "./Guards/auth-guard-service"

export function tokenGetter() {
  return localStorage.getItem("jwt");
}


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    MyTestsComponent,
    LoginComponent,
    TestFormComponent
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
        allowedDomains: ["localhost:9410"],
        disallowedRoutes:[]
        }
    }),

  ],
  providers: [AuthService, AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
