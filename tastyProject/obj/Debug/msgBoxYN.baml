   M S B A M L   `   `   `  ����G  CtastyProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:tastyProjecttastyProject  5   	       tastyProject.messageBox   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:tastyProject     5   	     � Title$   ��5   	   $	��200��6   $	��550��6    � WindowStartupLocation$ CenterScreen=�6+    � WindowStyle$ 
ToolWindow=�6P   .��5	      � $	��170��6   $	��500��6    � RowDefinitions 5
   
   �� 5      $��90 �6   6*   �� 5      $��80 �6   6*   .+�5   
   �� -   5         ��Name$ textMsg��5      $4��.?בטוחים שאתם רוצים להתנתק��6%   $��30(�6F   $1��+/tastyProject;component/Fonts/#Heebo Medium)�6T   $��	60,40,0,0q�6�    �� TextAlignment$ Center=�6�   $
��Wrap=�6�   
���   �$��2��6�   6�   �� 5   
   -   5         ��Name$
 yesB��5         CustomButtonStyle#��[  6    � Cursor$
 Hand��6J   $��1��6X   $��70��6e   $��30��6p   $
��Left=�6|   $��Center=�6�   $��
90,25,0,25q�6�   ���	#FFC32026#��� ��6�   #��� ��6,  .��6�  כן � 	Resources 5      	 �� 
TargetType
 �� Property �� Value��%^   '��       �� 5      "	 ��6   .�5      �� 
 �@��CornerRadius$ 5��65   5      5      5      �� 5   
   -   5        $	 noB��5      #��[  6   $
 Hand��6I   $��1��6W   $��70��6d   $��30��6o   $
��Left=�6{   $��Center=�6�   $��	5,25,0,25q�6�   ���	#FFC32026#��� ��6�   #��� ��6+  .��6�  לא 5      ��%^   '��       �� 5      "	 ��6   .�5      �� 
 �@��CornerRadius$ 5��65   5      5      5      5      5      