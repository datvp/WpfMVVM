   M S B A M L   `   `   `  ����N  JzSpace.Notification, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:zSpace.Notification.ConverterszSpace.Notification.Converters  5      1    +zSpace.Notification.Assets.NotificationItem   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      C
converters,clr-namespace:zSpace.Notification.Converters     5      -   5          >�Name$  NotificationWindow��5      
������  � 	Resources 5
       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $R Lpack://application:,,,/zSpace.Notification;component/Assets/CloseButton.xaml?�6%   %    5       � Triggers 5      :� 5   
    :� RoutedEvent$ Button.Click��6    :� 
SourceName$ CloseButton��62   .[�5      �� .��5      �� .��5      W�  �� 
TargetName$ NotificationWindow��6&    W� From$ 1X�6Q   		 W� To$	 0X�6Z   
 �� TargetProperty$
 (Grid.Opacity)�6a    g� Duration$ 0:0:0D�6�   5      5      5      ��5      �� 5   
    �� 
TargetType" >�6    �� Triggers 5      �� 5       �� Binding 6   ��  �� ElementName$ NotificationWindow�� �� Path$ Opacity� �� Value 06^   5      .��5!      � 
������ .+�5"   
   �� -   5        $��border��5"      ���#1E1E1E$��0q�64    �� CornerRadius$ 5��6H   $��10q�6Y    J� Effect 5#      3  -System.Windows.Media.Effects.DropShadowEffect  5$         ShadowDepth$ 0Q�6#      Opacity$	 0.8Q�63      
BlurRadius$ 5Q�6A   ._�5&      �  � RowDefinitions 5'      �� 5(      $
��Auto �6$   64   �� 5)      $��* �6$   61    � ColumnDefinitions 5+      �� 5,      $
��Auto �6'   66   �� 5-      $��* �6'   63   .+�5/      �� $��2��6   $��4q�51      $��80��6#   ��50      �� *ImgURL+ �� 	Converter 9   3zSpace.Notification.Converters.EmptyStringConverter   �� ConverterParameter$a [pack://application:,,,/zSpace.Notification;component/Resources/Images/notification-icon.png��51   0   �� 52      $��1��6   &   System.Windows.Media.TextOptions   TextRenderingMode$ 	ClearType=�54         TextFormattingMode$ Display=�55      
�������$��Arial)�57      $��14(�58      $
��Bold"�59      $��Center=�5:      $��2,4,4,2q�5;      $
��Wrap=�5<      $��CharacterEllipsis=�5=      ��53      �� $ Title�5=      �� 5>      -   5         ��Name$ CloseButton��5>      $��16��5?      $��16��5@      $��1��5A      $��Right=�5B      $��0,0,12,0q�5C         SystemCloseButton#��[  5E      ��5F      � 5G      $��10��6   $��12��6*   $z�1,0,0,1,0,1W�66   .+�5H      F� +����        � z ��j � z     �    ��j 	$��8��6@   $��7��6J   $��Center=�6U   $��Center=�6p   $	��1.5��5I   �   ��6?   �� *
Foreground+ �� RelativeSource ��  �� Mode$ FindAncestor=�  �� AncestorType$  ButtonL�6�   5J      5L      �� 5M      $��1��6   $��1��5N      $ 	ClearType=�5P      $ Display=�5Q      
�������$��Arial)�5S      $��Stretch=�5T      $��5q�5U      $
��Wrap=�5V      $��CharacterEllipsis=�5W      ��5O      �� $ Message�5W      5X      5Y      5Z      5[      