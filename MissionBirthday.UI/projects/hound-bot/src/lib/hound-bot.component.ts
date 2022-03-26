import { Component, OnInit } from '@angular/core';
import { HoundBotService } from './hound-bot.service';

@Component({
  selector: 'lib-hound-bot',
  template: `
  <div style="max-height:30rem">
    <ion-button (click)="startChat()" [hidden]="chatRunning">Start Chat</ion-button>
    <div id="webchat" role="main"></div>
  </div>
  `,
  styles: []
})
export class HoundBotComponent implements OnInit {
  public chatRunning: boolean = false;

  constructor(private botService: HoundBotService) { }

  ngOnInit() {
  }

  startChat() {
    this.chatRunning = true;
    const token$ = this.botService.getToken();

    token$.subscribe(res => {
          // AAAAAAAAHHHHHHHH
      const webChat: any = (window as any).WebChat;
      const styleSet = webChat.createStyleSet({
        bubbleBackground: '#0DD7FF',
        bubbleFromUserBackground: '#00FF9D',

        botAvatarInitials: 'HB',
        userAvatarInitials: 'US',
        rootHeight: '25rem',
        rootWidth: '20rem'
     });
      webChat.renderWebChat(
        {
          directLine: webChat.createDirectLine({ token: res.token }),
          userID: res.userId,
          locale: 'en-US',
          username: 'You',
          styleSet,
          styleOptions: {
            hideUploadButton: true,
          }
        },
        document.getElementById('webchat')
      );
    })
  }
}
