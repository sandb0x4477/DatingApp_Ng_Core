import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Input() registerModeFromHome: boolean;
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(private auth: AuthService) { }

  ngOnInit() {
  }

  register() {
    this.auth.register(this.model)
      .subscribe(res => {
        console.log('res', res);
        this.cancelRegister.emit(false);
      });
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
