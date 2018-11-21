import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/app/_models/user';
import { UsersService } from 'src/app/_services/users.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {
  @Input() user: User;

  constructor(private userService: UsersService,
    private auth: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }


  sendLike(user: User) {
    this.userService.sendLike(this.auth.decodedToken.nameid, user.id).subscribe(
      res => {
        this.alertify.success('You have liked: ' + user.knownAs);
      }, err => {
        this.alertify.error(err);
      });
  }

}
