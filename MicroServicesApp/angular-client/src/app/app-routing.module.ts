import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { MyTestsComponent } from './components/my-tests/my-tests.component';
import { LoginComponent } from './components/login/login.component';
import {TestFormComponent} from"./components/test-form/test-form.component"
import { AuthGuard } from "./Guards/auth-guard-service";
import {DetailsComponent} from "./components/testcomponents/details/details.component"



const routes: Routes = [
  { path: "", component: HomeComponent },
  { path: "MyTests", component: MyTestsComponent, canActivate: [AuthGuard] },
  { path: "Login", component: LoginComponent },
  { path: "CreateTest", component: TestFormComponent, canActivate: [AuthGuard] },
  { path: "Tests", component: DetailsComponent}


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
