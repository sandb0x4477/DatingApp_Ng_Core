import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  model: any = {};

  constructor(public auth: AuthService,
    private alertify: AlertifyService, private router: Router) { }

  ngOnInit() {
  }

  login() {
    this.auth.login(this.model)
      .subscribe(res => {
        this.alertify.success('Login Sucessful');
      }, error => {
        this.alertify.error(error);
      }, () => {
        this.router.navigate(['/members']);
      });
    }

    loggedIn() {
      return this.auth.loggedIn();
    }

    logOut() {
      localStorage.removeItem('token');
      this.alertify.message('Logged out');
      this.router.navigate(['/home']);
  }
}
