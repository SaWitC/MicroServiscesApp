import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { MyTestsComponent } from './components/my-tests/my-tests.component';
import { LoginComponent } from './components/login/login.component';
import {TestFormComponent} from"./components/test-form/test-form.component"
import {AuthGuard} from"./Guards/auth-guard-service";



const routes: Routes = [
  { path: "", component: HomeComponent },
  { path: "MyTests", component: MyTestsComponent },
  { path: "Login", component: LoginComponent },
  {path:"CreateTest",component:TestFormComponent,canActivate:[AuthGuard]}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
