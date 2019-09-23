import { animate, state, style, transition, trigger } from '@angular/animations';


export let slideFromBottom = trigger('slideFromBottom', [
  state('void', style({ 'padding-top': '20px', opacity: '0' })),
  state('*', style({ 'padding-top': '0px', opacity: '1' })),
  transition(':enter', [
    animate('0.33s ease-out', style({ opacity: '1', 'padding-top': '0px' }))
  ])
]);


export let slideFromUp = trigger('slideFromUp', [
  state('void', style({ 'margin-top': '-10px', opacity: '0' })),
  state('*', style({ 'margin-top': '0px', opacity: '1' })),
  transition(':enter', [
    animate('0.2s ease-out', style({ opacity: '1', 'margin-top': '0px' }))
  ])
]);

export let slideFromLeft = trigger('slideFromLeft', [
  state('void', style({ transform: 'translateX(-50px)', opacity: '0' })),
  state('*', style({  transform: 'translateX(0px)', opacity: '1' })),
  transition(':enter', [
    animate('0.2s ease-out', style({ opacity: '1', transform: 'translateX(0px)' }))
  ])
]);

export let slideFromRight = trigger('slideFromRight', [
  state('void', style({ transform: 'translateX(50px)', opacity: '0' })),
  state('*', style({  transform: 'translateX(0px)', opacity: '1' })),
  transition(':enter', [
    animate('0.2s ease-out', style({ opacity: '1', transform: 'translateX(0px)' }))
  ])
]);


export let bloom = trigger('bloom', [
  state('void', style({opacity: '0' })),
  state('*', style({ opacity: '1' })),
  transition(':enter', [
    animate('0.3s ease-out', style({ opacity: '1' }))
  ])
]);

