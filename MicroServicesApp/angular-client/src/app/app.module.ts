import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './components/home/home.component';
import { MyTestsComponent } from './components/my-tests/my-tests.component';
import {JwtModule} from "@auth0/angular-jwt";
import { HttpClientModule } from "@angular/common/http"
import { TestPassingServiceService } from './services/test-passing-service.service';
import {environment } from "src/environments/environment";
import { LoginComponent } from './components/login/login.component'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from "./services/auth.service";
import { TestFormComponent } from "./components/test-form/test-form.component"
import {AuthGuard} from "./Guards/auth-guard-service";
import { DetailsComponent } from './components/testcomponents/details/details.component';
import { RegisterComponent } from './components/register/register.component';
import { QuestFormComponent } from './components/questComponents/quest-form/quest-form.component'
import { QuestService } from "./services/quest.service";
import { InfoComponent } from './components/testcomponents/info/info.component';
import { TestPassingComponent } from './components/testcomponents/test-passing/test-passing.component';

export function tokenGetter() {
  return localStorage.getItem("jwt");
}


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    MyTestsComponent,
    LoginComponent,
    TestFormComponent,
    DetailsComponent,
    RegisterComponent,
    QuestFormComponent,
    InfoComponent,
    TestPassingComponent
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
  providers: [AuthService, AuthGuard, QuestService, TestPassingServiceService],
  bootstrap: [AppComponent]
})
export class AppModule { }
