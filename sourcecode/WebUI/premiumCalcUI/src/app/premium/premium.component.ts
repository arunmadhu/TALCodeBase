import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { DataService } from '../shared/services/data.service';
import { FormBuilder, FormGroup, Validators  } from '@angular/forms';

@Component({
  selector: 'app-premium',
  templateUrl: './premium.component.html',
  styleUrls: ['./premium.component.css']
})

export class PremiumComponent implements OnInit {

  premiumReqForm: FormGroup;
  submitted: boolean = false;
  occupations: ComboItem[];
  selectedOccupation: string;

  constructor(private titleService: Title, private formBuilder: FormBuilder, public dataService: DataService) {
    this.titleService.setTitle("Premium Finder");
  }

  ngOnInit() {
    this.premiumReqForm = this.formBuilder.group({
      userName: ['', Validators.required],
      dob: ['', Validators.required],
      occupation: ['', [Validators.required]],
      sumInsured: ['', [Validators.required, Validators.minLength(6)]]
    });

    this.loadOccupations();

  }

  loadOccupations() {
    this.dataService.getAllOccupations().subscribe((response: any) => {
      this.occupations = response;
      console.log(response);
      this.premiumReqForm.get('occupation').setValue(response[0].occupationId);
      this.selectedOccupation = response[0].occupationName;
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

interface ComboItem {
  value: string;
  viewValue: string;
}

