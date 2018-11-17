import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { MembersEditComponent } from '../members/members-edit/members-edit.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChanges implements CanDeactivate<MembersEditComponent> {
  canDeactivate(component: MembersEditComponent) {
    if (component.editForm.dirty) {
      return confirm('Are you sure you want to continue?');
    }
    return true;
  }
}
