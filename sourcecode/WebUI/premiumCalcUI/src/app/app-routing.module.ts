import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AboutComponent } from './about/about.component';
import { PremiumComponent } from './premium/premium.component';


const routes: Routes = [
  { path: "", pathMatch: "full", redirectTo: "premium" },
  { path: "premium", component: PremiumComponent },
  { path: "about", component: AboutComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
