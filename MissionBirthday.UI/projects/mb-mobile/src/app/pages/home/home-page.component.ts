import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent implements OnInit {
  public searchForm: FormGroup;

  constructor(private readonly router: Router) {
  }

  ngOnInit(): void {
    this.searchForm = new FormGroup({
      zipCode: new FormControl('', Validators.required)
    });
  }

  search(): void {
    if (this.searchForm.valid) {
      this.router.navigate(['/search', this.searchForm.value.zipCode]);
    } else {
      this.searchForm.markAllAsTouched();
    }
  }

  addResource(): void {
    this.router.navigate(['/add-resource']);
  }
}
