   M S B A M L   `   `   `  ����M  ICakeStorManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:CakeStorManagementCakeStorManagement  5   	   S.clr-namespace:CakeStorManagement.MyUserControl CakeStorManagement.MyUserControl  5   	   (    "CakeStorManagement.AddOutputWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:CakeStorManagement     5   	   V RMaterialDesignThemes.Wpf, Version=2.5.0.1205, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   
   7uc.clr-namespace:CakeStorManagement.MyUserControl  5   	   -   5        $��addoutputwindow��5   	      MaterialDesignBody#���   5	   	   $��Regular"�5
   	   $��13(�5   	   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStyle$
 None=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � Title$  Tạo phiếu xuất hàng��5   	   $	��300��6%   $	��400��62     AddOutputVM � DataContext# [ 5   	   .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      6   .+�5   
   � $��0��6   .+�5      3   -CakeStorManagement.MyUserControl.ControlBarUC  
 � Tag 6   �� *Title+	 �� ElementName$	 addoutputwindow��6U   5       � 5!   
   $��1��6    5"      �� 5#      6"   �� 5$      6"   �� 5%      6"   .+�5'      (  "System.Windows.Controls.DatePicker  $��0��6   $��Bottom=�6&   $	��150��6B   )  #MaterialDesignThemes.Wpf.HintAssist
   Hint$
 Ngày xuất��6N    ( $MaterialDesignFloatingHintDatePicker#��[ 6y      SelectedDateFormat$
 Long=�5(   F      SelectedDate 6"   �� *
DateOutput+6F   �� 5)      $��1��6   $
 Người xuất��6%    �� 
IsEditable . $
��0 15q�5*       �� DisplayMemberPath$ DisplayName��5+      $��Center=�6?   $	��Top=�6\   $	��150��6t    & "MaterialDesignFloatingHintComboBox#��[ 5-      ��5,      �� *
UserList+ �� SelectedItem 6@   �� *SelectedUser+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5-      �� 5.      $��2��6    $  MaterialDesignRaisedAccentButton#��[ 6"   $	��100��6\   $��5q�6i   $
��Xong��5/      $	��Top=�6,   ��50      �� *FinishCommand+��51      �� $	 addoutputwindow��52      53      54      