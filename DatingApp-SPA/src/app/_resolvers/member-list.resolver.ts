import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { User } from '../_models/user';
import { UsersService } from '../_services/users.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class MemberListResolver implements Resolve<User[]> {
  pageNumber = 1;
  pageSize = 5;

  constructor(private userService: UsersService,
    private alertify: AlertifyService, private router: Router) {}

  resolve(route: ActivatedRouteSnapshot): Observable<User[]> {
    return this.userService.getUsers(this.pageNumber, this.pageSize).pipe(
      catchError(err => {
        this.alertify.error('Problem retrieving data');
        this.router.navigate(['/home']);
        return of(null);
      })
    );
  }
}
