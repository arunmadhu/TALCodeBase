import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { DataService } from '../shared/services/data.service';
import { NgForm, FormBuilder, FormGroup, Validators  } from '@angular/forms';

@Component({
  selector: 'app-premium',
  templateUrl: './premium.component.html',
  styleUrls: ['./premium.component.css']
})
export class PremiumComponent implements OnInit {

  premiumReqForm: FormGroup;
  submitted: boolean = false;

  constructor(private titleService: Title, private formBuilder: FormBuilder, public dataService: DataService) {
    this.titleService.setTitle("Premium Finder");
  }

  ngOnInit() {
    this.premiumReqForm = this.formBuilder.group({
      userName: ['', Validators.required],
      dob: ['', Validators.required],
      occupation: ['', [Validators.required, Validators.email]],
      sumInsured: ['', [Validators.required, Validators.minLength(6)]]
    });

  }

  get form() { return this.premiumReqForm.controls; }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.premiumReqForm.invalid) {
      return;
    }

    alert('SUCCESS!! :-)')
  }

}
