import { Component ,OnInit} from '@angular/core';
import { AuthService } from "../app/services/auth.service";


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'angular-client';

  isAutenticated: boolean;

  constructor(public auth: AuthService) {
    
  }

  ngOnInit(): void {
    this.isAutenticated = this.auth.isAuhtenticated();
  }

}
