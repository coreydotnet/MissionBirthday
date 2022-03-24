import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-resource-form-page',
  templateUrl: './resource-form-page.component.html',
  styleUrls: ['./resource-form-page.component.scss']
})
export class ResourceFormPageComponent implements OnInit {
  public resourceForm: FormGroup;

  constructor() { }

  ngOnInit(): void {
    this.resourceForm = new FormGroup({
      organization: new FormControl(),
      phoneNumber: new FormControl(),
      email: new FormControl(),
      url: new FormControl(),
      location: new FormGroup({
        street1: new FormControl(),
        street2: new FormControl(),
        city: new FormControl(),
        state: new FormControl(),
        zip: new FormControl()
      }),
      hours: new FormControl(),
      details: new FormControl(),
      items: new FormControl([])
    })
  }

  addItem(input: HTMLInputElement): void {
    // I did not say it was OK to do this in real life
    this.resourceForm.controls.items.value.push(input.value);
    input.value = '';
  }

  removeItem(index): void {
    this.resourceForm.controls.items.value.splice(index, 1);
  }

  addEvent(): void {
    
  }
}
