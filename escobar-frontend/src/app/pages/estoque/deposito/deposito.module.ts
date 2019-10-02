import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateComponent } from './create/create.component';
import { DepositoRoutingModule } from './deposito-routing.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { FlexLayoutModule } from '@angular/flex-layout';
import { DepositoComponent } from './deposito.component';
import { MatTabsModule } from '@angular/material/tabs';
import { EditComponent } from './edit/edit.component';
import { MatTableModule } from '@angular/material/table';



@NgModule({
  declarations: [CreateComponent, DepositoComponent, EditComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    MatButtonModule,
    MatTabsModule,
    MatTableModule,
    MatInputModule,
    MatIconModule,
    FlexLayoutModule,
    DepositoRoutingModule
  ]
})
export class DepositoModule { }
