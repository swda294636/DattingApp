import { AuthGuard } from './_guards/auth.guard';
import { MessmagesComponent } from './messmages/messmages.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { HomeComponent } from './home/home.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListsComponent } from './lists/lists.component';

const routes: Routes = [
  {path:'' ,component: HomeComponent},
  {path:'' ,
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children:[
      {path:'members' ,component: MemberListComponent},
      {path:'members/:id' ,component: MemberDetailComponent},
      {path:'lists' ,component: ListsComponent},
      {path:'messages' ,component: MessmagesComponent},
    ]
  },

  {path:'**' ,component: HomeComponent, pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
