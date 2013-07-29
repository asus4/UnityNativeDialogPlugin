//
//  UNDialogManager.m
//  UnityDialogPlugin
//
//  Created by ibu on 12/10/09.
//  Copyright (c) 2012年 kayac. All rights reserved.
//

#import "UNDialogManager.h"

#define MakeStringCopy( _x_ ) ( _x_ != NULL && [_x_ isKindOfClass:[NSString class]] ) ? strdup( [_x_ UTF8String] ) : NULL

extern void UnitySendMessage(const char *, const char *, const char *);

extern "C" {
    int _showSelectDialog(const char *msg) {
        return [[UNDialogManager sharedManager]
                showSelectDialog:[NSString stringWithUTF8String:msg]];
    }
    
    int _showSelectTitleDialog(const char *title, const char *msg) {
        return [[UNDialogManager sharedManager]
                showSelectDialog:[NSString stringWithUTF8String:title]
                message:[NSString stringWithUTF8String:msg]];
    }
    
    int _showSubmitDialog(const char *msg) {
        return [[UNDialogManager sharedManager]
                showSubmitDialog:[NSString stringWithUTF8String:msg]];
    }
    
    int _showSubmitTitleDialog(const char *title, const char *msg) {
        return [[UNDialogManager sharedManager]
                showSubmitDialog:[NSString stringWithUTF8String:title]
                message:[NSString stringWithUTF8String:msg]];
    }
}



@implementation UNDialogManager

static UNDialogManager * shardDialogManager;

+ (UNDialogManager*) sharedManager {
    @synchronized(self) {
        if(shardDialogManager == nil) {
            shardDialogManager = [[self alloc]init];
        }
    }
    return shardDialogManager;
}

- (int) showSelectDialog:(NSString *)msg {
    UIAlertView *aleat = [[UIAlertView alloc] initWithTitle:nil message:msg delegate:self cancelButtonTitle:@"いいえ" otherButtonTitles:@"はい", nil];
    aleat.tag = ++_id;
    [aleat show];
    [aleat autorelease];
    return _id;
}

- (int) showSelectDialog:(NSString *)title message:(NSString*)msg {
    UIAlertView *aleat = [[UIAlertView alloc] initWithTitle:title message:msg delegate:self cancelButtonTitle:@"いいえ" otherButtonTitles:@"はい", nil];
    aleat.tag = ++_id;
    [aleat show];
    [aleat autorelease];
    return _id;
}

- (int) showSubmitDialog:(NSString *)msg {
    UIAlertView *aleat = [[UIAlertView alloc] initWithTitle:nil message:msg delegate:self cancelButtonTitle:nil otherButtonTitles:@"閉じる", nil];
    aleat.tag = ++_id;
    [aleat show];
    [aleat autorelease];
    return _id;
}

- (int) showSubmitDialog:(NSString *)title message:(NSString*)msg {
    UIAlertView *aleat = [[UIAlertView alloc] initWithTitle:title message:msg delegate:self cancelButtonTitle:nil otherButtonTitles:@"閉じる", nil];
    aleat.tag = ++_id;
    [aleat show];
    [aleat autorelease];
    return _id;
}

// delegate
- (void)alertView:(UIAlertView*)alertView clickedButtonAtIndex:(NSInteger)buttonIndex
{
    //NSLog(@"aleatView delegate. index tag %i", alertView.tag);
    NSString *tag = [NSString stringWithFormat:@"%d", alertView.tag];
    //char* tagChar = makeStringCopy([tag UTF8String]);
    
    switch (buttonIndex) {
        case 0:
            if(alertView.cancelButtonIndex == 0) {
                //NSLog(@"clicked cancel");
                UnitySendMessage("DialogManager", "OnCancel", tag.UTF8String);
            }
            else {
                //NSLog(@"clicked ok");
                UnitySendMessage("DialogManager", "OnSubmit", tag.UTF8String);
            }
            break;
        case 1:
            if(alertView.cancelButtonIndex == 1) {
                //NSLog(@"clicked cancel");
                UnitySendMessage("DialogManager", "OnCancel", tag.UTF8String);
            }
            else {
                //NSLog(@"clicked ok");
                UnitySendMessage("DialogManager", "OnSubmit", tag.UTF8String);
            }
            break;
        default:
            break;
    }
}

@end


