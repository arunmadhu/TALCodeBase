import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { DataService } from '../shared/services/data.service';
import { FormBuilder, FormGroup, Validators  } from '@angular/forms';
import { premiumRequest } from '../shared/model/premiumRequest';
import { DateAdapter } from '@angular/material';

@Component({
  selector: 'app-premium',
  templateUrl: './premium.component.html',
  styleUrls: ['./premium.component.css']
})

export class PremiumComponent implements OnInit {

  premiumReqForm: FormGroup;
  submitted: boolean = false;
  premiumFound: boolean = false;
  invalidSumInsured: boolean = false;
  isfutureDate: boolean = false;
  occupations: any[];
  rating: string;
  premium: number;
  age: number;
  isActionProgress: boolean  = false;
  maxDate: Date = new Date();
  minDate: Date;

  constructor(private titleService: Title, private formBuilder: FormBuilder, public dataService: DataService, private dateAdapter: DateAdapter<Date>) {
    this.titleService.setTitle("Premium Finder");
    this.dateAdapter.setLocale('en-GB'); 
  }

  ngOnInit() {
    this.premiumReqForm = this.formBuilder.group({
      userName: ['', Validators.required],
      dob: ['', Validators.required],
      occupation: ['', [Validators.required]],
      sumInsured: ['', [Validators.required]]
    });

    this.minDate = new Date();
    this.minDate.setFullYear(this.minDate.getFullYear() - 150);

    this.loadOccupations();

  }

  loadOccupations() {
    this.isActionProgress = true;

    this.dataService.getAllOccupations().subscribe((response: any) => {
      this.occupations = response;
      this.premiumReqForm.get('occupation').setValue(response[0].occupationId);
      this.isActionProgress = false;
    });
  }

  get form() { return this.premiumReqForm.controls; }

  onSubmit() {
    this.submitted = true;
    this.premiumFound = false;
    this.invalidSumInsured = false;
    this.isfutureDate = false;

    if (this.premiumReqForm.invalid) {
      return;
    }
    else{
        this.validateForm();
        if(this.invalidSumInsured || this.isfutureDate)
          return;
    }
    let request: premiumRequest = {
        deathSumInsured :this.premiumReqForm.get('sumInsured').value,
        occupationId: this.premiumReqForm.get('occupation').value,
        userName: this.premiumReqForm.get('userName').value,
        dateOfBirth: this.premiumReqForm.get('dob').value
    };

    this.isActionProgress = true;

    this.dataService.calculatePremium(request).subscribe((response: any) => {
          this.premiumFound = true;
          this.rating = response.ratingDesc.trim();
          this.premium = response.deathPremium;
          this.age = response.age;
          
          this.isActionProgress = false;
    });
  }

  validateForm(){
      if( this.premiumReqForm.get('sumInsured').value <= 0){
        this.invalidSumInsured = true;
      }
      this.checkDob();
  }

  checkDob()  {
    this.isfutureDate = false;
    if( this.premiumReqForm.get('dob').value){
      let inputDate = new Date(this.premiumReqForm.get('dob').value).setHours(0,0,0,0);
      let today = new Date().setHours(0,0,0,0);
      if(inputDate > today)
          this.isfutureDate = true;
    }

  }
}

