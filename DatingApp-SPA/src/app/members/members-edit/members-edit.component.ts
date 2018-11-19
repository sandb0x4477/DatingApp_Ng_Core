import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from 'src/app/_models/user';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { NgForm } from '@angular/forms';
import { UsersService } from 'src/app/_services/users.service';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-members-edit',
  templateUrl: './members-edit.component.html',
  styleUrls: ['./members-edit.component.css']
})
export class MembersEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;

  user: User;
  photoUrl: string;

  constructor(private route: ActivatedRoute,
    private alertify: AlertifyService,
    private userService: UsersService, private auth: AuthService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data['user'];
    });
    this.auth.currentPhotoUrl.subscribe(photoUrl => this.photoUrl = photoUrl);
  }

  updateUser() {
    this.userService.updateUser(this.auth.decodedToken.nameid, this.user)
    .subscribe(res => {
      this.alertify.success('Profile updated');
      this.editForm.reset(this.user);
    }, err => {
      this.alertify.error(err);
    });
  }

  updateMainPhoto(event) {
    this.user.photoUrl = event;
  }
}
