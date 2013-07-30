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
    
    void _dissmissDialog(const int theID){
        [[UNDialogManager sharedManager] dissmissDialog:theID];
    }

    void _setLabel(const char *decide, const char *cancel, const char *close) {
        [[UNDialogManager sharedManager] 
            setLabelTitleWithDecide:[NSString stringWithUTF8String:decide]
                             cancel:[NSString stringWithUTF8String:cancel]
                              close:[NSString stringWithUTF8String:close]];
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

- (id) init {
    alerts = [NSMutableDictionary dictionary];
    [alerts retain];

    return [super init];
}

- (void) dealloc {
    [decideLabel release];
    [cancelLabel release];
    [closeLabel release];
    [alerts release];
    [super dealloc];
}

- (int) showSelectDialog:(NSString *)msg {
    UIAlertView *alert = [[UIAlertView alloc] initWithTitle:nil message:msg delegate:self cancelButtonTitle:cancelLabel otherButtonTitles:decideLabel, nil];
    alert.tag = ++_id;
    [alert show];
    [alert autorelease];
    
    [alerts setObject:alert forKey:[NSNumber numberWithInt:_id]];
    return _id;
}

- (int) showSelectDialog:(NSString *)title message:(NSString*)msg {
    UIAlertView *alert = [[UIAlertView alloc] initWithTitle:title message:msg delegate:self cancelButtonTitle:cancelLabel otherButtonTitles:decideLabel, nil];
    alert.tag = ++_id;
    [alert show];
    [alert autorelease];
    
    [alerts setObject:alert forKey:[NSNumber numberWithInt:_id]];
    return _id;
}

- (int) showSubmitDialog:(NSString *)msg {
    UIAlertView *alert = [[UIAlertView alloc] initWithTitle:nil message:msg delegate:self cancelButtonTitle:nil otherButtonTitles:closeLabel, nil];
    alert.tag = ++_id;
    [alert show];
    [alert autorelease];
    
    [alerts setObject:alert forKey:[NSNumber numberWithInt:_id]];
    return _id;
}

- (int) showSubmitDialog:(NSString *)title message:(NSString*)msg {
    UIAlertView *alert = [[UIAlertView alloc] initWithTitle:title message:msg delegate:self cancelButtonTitle:nil otherButtonTitles:closeLabel, nil];
    alert.tag = ++_id;
    [alert show];
    [alert autorelease];
    
    [alerts setObject:alert forKey:[NSNumber numberWithInt:_id]];
    return _id;
}

- (void) dissmissDialog:(int)theID {
    UIAlertView *alert = alerts[[NSNumber numberWithInt:theID]];
    [alert dismissWithClickedButtonIndex:0 animated:YES];
    [alerts removeObjectForKey:[NSNumber numberWithInt:theID]];
}

- (void) setLabelTitleWithDecide:(NSString*)decide cancel:(NSString*)cancel close:(NSString*) close {
    [decideLabel release];
    [cancelLabel release];
    [closeLabel release];

    decideLabel = [NSString stringWithString:decide];
    cancelLabel = [NSString stringWithString:cancel];
    closeLabel = [NSString stringWithString:close];
    
    [decideLabel retain];
    [cancelLabel retain];
    [closeLabel retain];
}

// delegate
- (void)alertView:(UIAlertView*)alertView clickedButtonAtIndex:(NSInteger)buttonIndex
{
    NSString *tag = [NSString stringWithFormat:@"%d", alertView.tag];
    
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
    
    [alerts removeObjectForKey:[NSNumber numberWithInteger:alertView.tag]];
}

@end