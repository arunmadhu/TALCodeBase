import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-premium',
  templateUrl: './premium.component.html',
  styleUrls: ['./premium.component.css']
})
export class PremiumComponent implements OnInit {

  constructor(private titleService: Title) {
    this.titleService.setTitle("Premium Finder");
  }

  ngOnInit() {
  }

}
